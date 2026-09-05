<template>
  <div :class="['network-status', isOnline ? 'status-online' : 'status-offline']">
    <div class="status-dot"></div>
    <span>{{ isOnline ? 'Online' : 'Offline - Operating with limited connectivity' }}</span>
  </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'

const isOnline = ref(true)

const updateOnlineStatus = () => {
  isOnline.value = navigator.onLine
}

onMounted(() => {
  if (typeof window !== 'undefined') {
    isOnline.value = navigator.onLine
    window.addEventListener('online', updateOnlineStatus)
    window.addEventListener('offline', updateOnlineStatus)
  }
})

onUnmounted(() => {
  if (typeof window !== 'undefined') {
    window.removeEventListener('online', updateOnlineStatus)
    window.removeEventListener('offline', updateOnlineStatus)
  }
})
</script>
