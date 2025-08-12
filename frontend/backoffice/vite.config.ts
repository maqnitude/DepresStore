import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import fs from "fs";
import path from "path";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000,
    https: {
      key: fs.readFileSync(path.resolve(__dirname, "cert/localhost-key.pem")),
      cert: fs.readFileSync(path.resolve(__dirname, "cert/localhost-cert.pem")),
    }
  },
});
