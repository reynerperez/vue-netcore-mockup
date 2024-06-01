import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { fileURLToPath, URL } from "url";
import Vuetify from 'vite-plugin-vuetify'
import ViteFonts from 'unplugin-fonts/vite'


// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    Vuetify(),
    ViteFonts({
      google: {
        families: [{
          name: 'Roboto',
          styles: 'wght@100;300;400;500;700;900',
        }],
      },
    }),],
  resolve: {
    alias: [
      { find: '@', replacement: fileURLToPath(new URL('./src', import.meta.url)) }
    ],
  },
  server: {
    port: 3000,
  },
})
