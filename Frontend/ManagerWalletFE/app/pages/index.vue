<template>
  <div>
    Redirecting...
  </div>
</template>

<script setup>
import { definePageMeta, navigateTo } from '#imports'
import { useAuth } from '~/composables/useAuth'

definePageMeta({
  middleware: [
    function (to, from) {
      const { token } = useAuth()
      if (token.value) {
        return navigateTo('/dashboard')
      }
      return navigateTo('/login')
    }
  ]
})
</script>
