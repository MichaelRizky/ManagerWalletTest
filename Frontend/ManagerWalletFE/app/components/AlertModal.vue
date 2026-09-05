<template>
  <div v-if="isOpen" class="modal-overlay animate-fade-in" @click="onClose">
    <div class="glass-card modal-content" @click.stop>
      <div class="icon-container" :class="type === 'error' ? 'text-error' : 'text-success'">
        <!-- Check icon for success -->
        <svg v-if="type === 'success'" xmlns="http://www.w3.org/2000/svg" width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"></path>
          <polyline points="22 4 12 14.01 9 11.01"></polyline>
        </svg>
        <!-- X icon for error -->
        <svg v-else xmlns="http://www.w3.org/2000/svg" width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <circle cx="12" cy="12" r="10"></circle>
          <line x1="15" y1="9" x2="9" y2="15"></line>
          <line x1="9" y1="9" x2="15" y2="15"></line>
        </svg>
      </div>

      <h3 class="text-center mb-2">{{ title }}</h3>
      <p class="text-center" style="color: var(--text-secondary); margin-bottom: 1.5rem;">{{ message }}</p>
      
      <div class="modal-actions">
        <button class="btn btn-primary" @click="onClose" style="width: 100%;">OK</button>
      </div>
    </div>
  </div>
</template>

<script setup>
defineProps({
  isOpen: {
    type: Boolean,
    default: false
  },
  type: {
    type: String,
    default: 'success' // 'success' or 'error'
  },
  title: {
    type: String,
    default: 'Notification'
  },
  message: {
    type: String,
    default: ''
  }
})

const emit = defineEmits(['close'])

const onClose = () => {
  emit('close')
}
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.6);
  backdrop-filter: blur(4px);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 10000;
}

.modal-content {
  width: 90%;
  max-width: 350px;
  padding: 2rem;
  box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.5);
  animation: scaleIn 0.3s ease-out forwards;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.icon-container {
  margin-bottom: 1rem;
}

.text-error {
  color: var(--error);
}

.text-success {
  color: var(--success);
}

@keyframes scaleIn {
  from { opacity: 0; transform: scale(0.95); }
  to { opacity: 1; transform: scale(1); }
}
</style>
