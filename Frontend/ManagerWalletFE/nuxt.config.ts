process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0'

// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2025-07-15',
  devtools: { enabled: true },
  css: ['~~/assets/css/main.css'],
  runtimeConfig: {
    public: {
      // Dikosongkan agar frontend memanggil http://localhost:3000/api yang kemudian diproxy ke backend
      apiBase: process.env.NUXT_PUBLIC_API_BASE || ''
    }
  },
  routeRules: {
    // Meneruskan semua request dari /api ke backend .NET untuk menghindari masalah CORS
    '/api/**': { proxy: 'https://localhost:44349/api/**' }
  }
})
