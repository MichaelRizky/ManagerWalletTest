import { ref } from 'vue'
import { useRouter } from 'nuxt/app'
import { useRuntimeConfig, useCookie } from '#imports'

export const useAuth = () => {
  const router = useRouter()
  const config = useRuntimeConfig()
  
  // State
  const token = useCookie('auth_token', { maxAge: 15 * 60 }) // 15 minutes
  const userEmail = useCookie('auth_email')
  const username = useCookie('auth_username')
  const isLoading = ref(false)
  const error = ref('')

  // Inactivity Timer
  let inactivityTimer = null
  const INACTIVITY_LIMIT = 15 * 60 * 1000 // 15 minutes

  const resetInactivityTimer = () => {
    if (inactivityTimer) clearTimeout(inactivityTimer)
    inactivityTimer = setTimeout(() => {
      logout()
    }, INACTIVITY_LIMIT)
  }

  const setupActivityListeners = () => {
    if (typeof window !== 'undefined') {
      window.addEventListener('mousemove', resetInactivityTimer)
      window.addEventListener('keypress', resetInactivityTimer)
      window.addEventListener('click', resetInactivityTimer)
      window.addEventListener('scroll', resetInactivityTimer)
      resetInactivityTimer()
    }
  }

  const removeActivityListeners = () => {
    if (typeof window !== 'undefined') {
      window.removeEventListener('mousemove', resetInactivityTimer)
      window.removeEventListener('keypress', resetInactivityTimer)
      window.removeEventListener('click', resetInactivityTimer)
      window.removeEventListener('scroll', resetInactivityTimer)
      if (inactivityTimer) clearTimeout(inactivityTimer)
    }
  }

  const register = async (email, password, confirmPassword) => {
    isLoading.value = true
    error.value = ''
    try {
      const response = await $fetch(`${config.public.apiBase}/api/Auth/register`, {
        method: 'POST',
        body: { email, password, confirmPassword }
      })
      return true
    } catch (err) {
      error.value = err.data?.message || err.data?.title || (typeof err.data === 'string' ? err.data : null) || err.message || 'Registration failed'
      return false
    } finally {
      isLoading.value = false
    }
  }

  const login = async (email, password) => {
    isLoading.value = true
    error.value = ''
    try {
      const response = await $fetch(`${config.public.apiBase}/api/Auth/login`, {
        method: 'POST',
        body: { email, password }
      })
      token.value = response.token
      userEmail.value = email
      
      // Need to fetch email from balance API or store it separately if login doesn't return it
      // Based on docs, login only returns token. Let's keep username as identifier for now
      
      setupActivityListeners()
      return true
    } catch (err) {
      error.value = err.data?.message || err.data?.title || (typeof err.data === 'string' ? err.data : null) || err.message || 'Login failed'
      return false
    } finally {
      isLoading.value = false
    }
  }

  const logout = () => {
    token.value = null
    userEmail.value = null
    username.value = null
    removeActivityListeners()
    router.push('/login')
  }

  return {
    token,
    userEmail,
    username,
    isLoading,
    error,
    register,
    login,
    logout,
    setupActivityListeners,
    removeActivityListeners
  }
}
