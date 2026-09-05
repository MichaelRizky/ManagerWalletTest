<template>
  <div class="container" style="justify-content: center; align-items: center;">
    <div class="glass-card animate-fade-in" style="width: 100%; max-width: 400px;">
      <h2 class="text-center">Welcome Back</h2>
      <p class="text-center mb-4" style="color: var(--text-secondary)">Log in to your Manager Wallet</p>
      
      <form @submit.prevent="handleLogin">
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
            placeholder="Enter your password"
          />
        </div>
        
        <button type="submit" class="btn btn-primary mt-4" :disabled="isLoading">
          <span v-if="isLoading" class="spinner"></span>
          {{ isLoading ? 'Logging in...' : 'Login' }}
        </button>
      </form>
      
      <p class="text-center mt-4" style="font-size: 0.9rem;">
        Don't have an account? <NuxtLink to="/register">Register here</NuxtLink>
      </p>
    </div>

    <AlertModal
      :isOpen="!!error"
      type="error"
      title="Login Failed"
      :message="error"
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
const router = useRouter()
const { login, isLoading, error } = useAuth()

const closeAlert = () => {
  error.value = ''
}

const handleLogin = async () => {
  const success = await login(email.value, password.value)
  if (success) {
    router.push('/dashboard')
  }
}
</script>
