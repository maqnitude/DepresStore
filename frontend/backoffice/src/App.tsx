import { Route, Routes } from "react-router";
import MainLayout from "./layouts/MainLayout";
import Dashboard from "./pages/Dashboard";
import Home from "./pages/Home";
import { createTheme, CssBaseline, ThemeProvider } from "@mui/material";
import ProtectedRoute from "./components/ProtectedRoute";
import LoginCallback from "./features/auth/LoginCallback";
import LogoutCallback from "./features/auth/LogoutCallback";
import Login from "./pages/Login";
import PublicOnlyRoute from "./components/PublicOnlyRoute";

const theme = createTheme({
  colorSchemes: {
    dark: true,
  },
});

function App() {
  return (
    <ThemeProvider theme={theme} defaultMode="dark">
      <CssBaseline />
      <Routes>
        <Route
          path="login"
          element={
            <PublicOnlyRoute>
              <Login />
            </PublicOnlyRoute>
          }
        />
        <Route path="login-callback" element={<LoginCallback />} />
        <Route path="logout-callback" element={<LogoutCallback />} />

        <Route element={<MainLayout />}>
          <Route
            index
            element={
              <ProtectedRoute>
                <Home />
              </ProtectedRoute>
            }
          />
          <Route
            path="dashboard"
            element={
              <ProtectedRoute>
                <Dashboard />
              </ProtectedRoute>
            }
          />
        </Route>
      </Routes>
    </ThemeProvider>
  );
}

export default App;
