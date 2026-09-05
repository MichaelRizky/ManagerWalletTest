<template>
  <div class="container animate-fade-in" style="padding-top: 3rem;">
    <div class="header">
      <div>
        <h1 style="font-size: 2rem; margin-bottom: 0.5rem;">Hello {{ userEmail }}, welcome back</h1>
        <p style="color: var(--text-secondary);">Manage your wallet and transfers securely.</p>
      </div>
      <button @click="logout" class="btn btn-outline" style="padding: 0.5rem 1rem;">Logout</button>
    </div>

    <div class="dashboard-grid mt-4">
      <!-- Balance Section -->
      <div class="balance-card">
        <h3>Current Balance</h3>
        <div class="balance-amount">
          <span v-if="isLoading && !balance">Loading...</span>
          <span v-else>Rp {{ formatCurrency(balance) }}</span>
        </div>
        
        <form @submit.prevent="handleTopup" class="mt-4" style="background: rgba(0,0,0,0.1); padding: 1.5rem; border-radius: 12px;">
          <h4 class="mb-2">Top-up Balance</h4>
          <div class="form-group" style="margin-bottom: 1rem;">
            <input 
              type="text" 
              v-model="displayTopupAmount" 
              @input="formatTopup"
              class="form-control" 
              placeholder="Amount (e.g. 50.000)"
              required
              style="background: rgba(255,255,255,0.1); border-color: rgba(255,255,255,0.2); color: white;"
            />
          </div>
          <button type="submit" class="btn" style="background: white; color: var(--primary);" :disabled="isLoading">
            {{ isLoading ? 'Processing...' : 'Top Up Now' }}
          </button>
        </form>
      </div>

      <!-- Transfer Section -->
      <div class="glass-card">
        <h3>Transfer Money</h3>
        <p class="mb-4" style="color: var(--text-secondary); font-size: 0.9rem;">Send money to another user instantly.</p>
        
        <form @submit.prevent="handleTransfer">
          <div class="form-group">
            <label for="recipient">Recipient Email</label>
            <input 
              type="email" 
              id="recipient" 
              v-model="transferRecipient" 
              class="form-control" 
              placeholder="Enter recipient email"
              required
            />
          </div>

          <div class="form-group">
            <label for="amount">Amount</label>
            <input 
              type="text" 
              id="amount" 
              v-model="displayTransferAmount" 
              @input="formatTransfer"
              class="form-control" 
              placeholder="Enter amount"
              required
            />
          </div>
          
          <div class="form-group">
            <label for="notes">Notes (Optional)</label>
            <input 
              type="text" 
              id="notes" 
              v-model="transferNotes" 
              class="form-control" 
              placeholder="What is this for?"
            />
          </div>

          <button type="submit" class="btn btn-primary mt-4" :disabled="isLoading">
            <span v-if="isLoading" class="spinner"></span>
            {{ isLoading ? 'Processing Transfer...' : 'Send Money' }}
          </button>
        </form>
      </div>
    </div>

    <!-- Confirm Modals -->
    <ConfirmModal 
      :isOpen="showTopupConfirm" 
      title="Confirm Top-up"
      :message="`Are you sure you want to top-up Rp ${displayTopupAmount}?`"
      @confirm="executeTopup"
      @cancel="showTopupConfirm = false"
    />

    <ConfirmModal 
      :isOpen="showTransferConfirm" 
      title="Confirm Transfer"
      :message="`Are you sure you want to transfer Rp ${displayTransferAmount} to ${transferRecipient}?`"
      @confirm="executeTransfer"
      @cancel="showTransferConfirm = false"
    />

    <AlertModal
      :isOpen="!!error || !!successMessage"
      :type="error ? 'error' : 'success'"
      :title="error ? 'Action Failed' : 'Success'"
      :message="error || successMessage"
      @close="closeAlert"
    />
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useAuth } from '~/composables/useAuth'
import { useWallet } from '~/composables/useWallet'

definePageMeta({
  middleware: 'auth'
})

const { userEmail, logout } = useAuth()
const { balance, fetchBalance, topup, transfer, isLoading, error, successMessage } = useWallet()

const topupAmount = ref(0)
const displayTopupAmount = ref('')
const transferRecipient = ref('')
const transferAmount = ref(0)
const displayTransferAmount = ref('')
const transferNotes = ref('')

const showTopupConfirm = ref(false)
const showTransferConfirm = ref(false)

const closeAlert = () => {
  error.value = ''
  successMessage.value = ''
}

const formatCurrency = (val) => {
  return Number(val).toLocaleString('id-ID')
}

const formatTopup = (e) => {
  let val = e.target.value.replace(/\D/g, '')
  if (!val) {
    topupAmount.value = 0
    displayTopupAmount.value = ''
    return
  }
  topupAmount.value = parseInt(val, 10)
  displayTopupAmount.value = Number(val).toLocaleString('id-ID')
}

const formatTransfer = (e) => {
  let val = e.target.value.replace(/\D/g, '')
  if (!val) {
    transferAmount.value = 0
    displayTransferAmount.value = ''
    return
  }
  transferAmount.value = parseInt(val, 10)
  displayTransferAmount.value = Number(val).toLocaleString('id-ID')
}

const handleTopup = () => {
  if (!topupAmount.value) return
  showTopupConfirm.value = true
}

const executeTopup = async () => {
  showTopupConfirm.value = false
  const success = await topup(topupAmount.value)
  if (success) {
    topupAmount.value = 0
    displayTopupAmount.value = ''
  }
}

const handleTransfer = () => {
  if (!transferRecipient.value || !transferAmount.value) return
  showTransferConfirm.value = true
}

const executeTransfer = async () => {
  showTransferConfirm.value = false
  const success = await transfer(transferRecipient.value, transferAmount.value, transferNotes.value)
  if (success) {
    transferRecipient.value = ''
    transferAmount.value = 0
    displayTransferAmount.value = ''
    transferNotes.value = ''
  }
}

onMounted(() => {
  fetchBalance()
})
</script>
