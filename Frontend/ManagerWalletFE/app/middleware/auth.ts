import { defineNuxtRouteMiddleware, navigateTo } from '#app'
import { useAuth } from '~/composables/useAuth'

export default defineNuxtRouteMiddleware((to, from) => {
  const { token } = useAuth()

  // If the route is protected and the user is not authenticated
  if (to.path === '/dashboard' && !token.value) {
    return navigateTo('/login')
  }

  // If the user is authenticated and tries to access login or register
  if ((to.path === '/login' || to.path === '/register') && token.value) {
    return navigateTo('/dashboard')
  }
  
  // If hitting the root path, redirect appropriately
  if (to.path === '/') {
    if (token.value) {
      return navigateTo('/dashboard')
    } else {
      return navigateTo('/login')
    }
  }
})
