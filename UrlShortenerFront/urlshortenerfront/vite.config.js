import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    optimizeDeps: {
        include: ["cookie"]
    },
    server: {
        port: 54406,
    }
})