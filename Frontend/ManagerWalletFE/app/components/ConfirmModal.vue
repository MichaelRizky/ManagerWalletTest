<template>
  <div v-if="isOpen" class="modal-overlay animate-fade-in">
    <div class="glass-card modal-content" @click.stop>
      <h3 class="mb-2">{{ title }}</h3>
      <p style="color: var(--text-secondary); margin-bottom: 1.5rem;">{{ message }}</p>
      
      <div class="modal-actions">
        <button class="btn btn-outline" @click="onCancel" style="flex: 1;">Cancel</button>
        <button class="btn btn-primary" @click="onConfirm" style="flex: 1;">Confirm</button>
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
  title: {
    type: String,
    default: 'Confirm Action'
  },
  message: {
    type: String,
    default: 'Are you sure you want to proceed?'
  }
})

const emit = defineEmits(['confirm', 'cancel'])

const onConfirm = () => {
  emit('confirm')
}

const onCancel = () => {
  emit('cancel')
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
  z-index: 9999;
}

.modal-content {
  width: 90%;
  max-width: 400px;
  padding: 2rem;
  box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.5);
  animation: scaleIn 0.3s ease-out forwards;
}

.modal-actions {
  display: flex;
  gap: 1rem;
}

@keyframes scaleIn {
  from { opacity: 0; transform: scale(0.95); }
  to { opacity: 1; transform: scale(1); }
}
</style>
