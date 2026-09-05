import { ref } from 'vue'
import { useRuntimeConfig, useCookie } from '#imports'
import { useAuth } from './useAuth'

export const useWallet = () => {
  const config = useRuntimeConfig()
  const { token, logout } = useAuth()
  
  const balance = ref(0)
  const isLoading = ref(false)
  const error = ref('')
  const successMessage = ref('')

  const getHeaders = () => {
    return {
      Authorization: `Bearer ${token.value}`
    }
  }

  const handleUnauthorized = (err) => {
    if (err.status === 401 || err.response?.status === 401) {
      logout()
    }
  }

  const fetchBalance = async () => {
    isLoading.value = true
    error.value = ''
    try {
      const response = await $fetch(`${config.public.apiBase}/api/Wallet/balance`, {
        headers: getHeaders()
      })
      balance.value = response.balance
      return response
    } catch (err) {
      handleUnauthorized(err)
      error.value = err.data?.message || err.data?.title || (typeof err.data === 'string' ? err.data : null) || err.message || 'Failed to fetch balance'
      return null
    } finally {
      isLoading.value = false
    }
  }

  const transfer = async (recipientEmail, amount, notes) => {
    isLoading.value = true
    error.value = ''
    successMessage.value = ''
    try {
      const response = await $fetch(`${config.public.apiBase}/api/Wallet/transfer`, {
        method: 'POST',
        headers: getHeaders(),
        body: { recipientEmail, amount: Number(amount), notes }
      })
      successMessage.value = 'Transfer successful'
      balance.value = response.remainingBalance
      return true
    } catch (err) {
      handleUnauthorized(err)
      error.value = err.data?.message || err.data?.title || (typeof err.data === 'string' ? err.data : null) || err.message || 'Transfer failed'
      return false
    } finally {
      isLoading.value = false
    }
  }

  const topup = async (amount) => {
    isLoading.value = true
    error.value = ''
    successMessage.value = ''
    try {
      const response = await $fetch(`${config.public.apiBase}/api/Wallet/topup`, {
        method: 'POST',
        headers: getHeaders(),
        body: { amount: Number(amount) }
      })
      successMessage.value = 'Top-up successful'
      balance.value = response.newBalance
      return true
    } catch (err) {
      handleUnauthorized(err)
      error.value = err.data?.message || err.data?.title || (typeof err.data === 'string' ? err.data : null) || err.message || 'Top-up failed'
      return false
    } finally {
      isLoading.value = false
    }
  }

  return {
    balance,
    isLoading,
    error,
    successMessage,
    fetchBalance,
    transfer,
    topup
  }
}
