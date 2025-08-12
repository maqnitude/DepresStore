import { User } from "oidc-client-ts";
import { useEffect, useState, type ReactNode } from "react";
import { userManager } from "./AuthService";
import { AuthContext } from "./AuthContext";

export default function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    userManager.getUser().then(setUser);
  }, []);

  const signinRedirect = () => userManager.signinRedirect();
  const signoutRedirect = () => userManager.signoutRedirect();

  return (
    <AuthContext value={{ user, signinRedirect, signoutRedirect }}>
      {children}
    </AuthContext>
  );
}
