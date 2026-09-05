<template>
  <div class="container" style="justify-content: center; align-items: center; padding: 4rem 2rem;">
    <div class="glass-card animate-fade-in" style="width: 100%; max-width: 450px;">
      <h2 class="text-center">Create an Account</h2>
      <p class="text-center mb-4" style="color: var(--text-secondary)">Join Manager Wallet today</p>
      
      <form @submit.prevent="handleRegister" v-if="!success">
        <div class="form-group">
          <label for="email">Email</label>
          <input 
            type="email" 
            id="email" 
            v-model="email" 
            class="form-control" 
            required 
            placeholder="Enter your email"
          />
        </div>
        
        <div class="form-group">
          <label for="password">Password</label>
          <input 
            type="password" 
            id="password" 
            v-model="password" 
            class="form-control" 
            required 
            placeholder="Create a password"
          />
        </div>
        
        <div class="form-group">
          <label for="confirmPassword">Confirm Password</label>
          <input 
            type="password" 
            id="confirmPassword" 
            v-model="confirmPassword" 
            class="form-control" 
            required 
            placeholder="Confirm your password"
          />
        </div>
        
        <button type="submit" class="btn btn-primary mt-4" :disabled="isLoading">
          <span v-if="isLoading" class="spinner"></span>
          {{ isLoading ? 'Registering...' : 'Register' }}
        </button>
      </form>
      
      <p class="text-center mt-4" style="font-size: 0.9rem;" v-if="!success">
        Already have an account? <NuxtLink to="/login">Log in here</NuxtLink>
      </p>
    </div>
    
    <ConfirmModal 
      :isOpen="showConfirm" 
      title="Registration Confirmation"
      :message="`Are you sure you want to register an account with email ${email}?`"
      @confirm="executeRegister"
      @cancel="showConfirm = false"
    />

    <AlertModal
      :isOpen="!!error || success"
      :type="error ? 'error' : 'success'"
      :title="error ? 'Registration Failed' : 'Success'"
      :message="error || 'Registration successful! Redirecting to login...'"
      @close="closeAlert"
    />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'nuxt/app'
import { useAuth } from '~/composables/useAuth'

definePageMeta({
  middleware: 'auth'
})

const email = ref('')
const password = ref('')
const confirmPassword = ref('')
const success = ref(false)
const validationError = ref('')
const showConfirm = ref(false)

const router = useRouter()
const { register, isLoading, error: authError } = useAuth()

const error = computed({
  get: () => validationError.value || authError.value,
  set: (val) => {
    validationError.value = val
    authError.value = val
  }
})

const closeAlert = () => {
  error.value = ''
}

const handleRegister = () => {
  validationError.value = ''
  
  if (password.value !== confirmPassword.value) {
    validationError.value = 'Passwords do not match.'
    return
  }
  
  showConfirm.value = true
}

const executeRegister = async () => {
  showConfirm.value = false
  const isSuccess = await register(email.value, password.value, confirmPassword.value)
  if (isSuccess) {
    success.value = true
    setTimeout(() => {
      router.push('/login')
    }, 2000)
  }
}
</script>
