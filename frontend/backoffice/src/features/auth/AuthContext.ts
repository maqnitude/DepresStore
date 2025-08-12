import type { User } from "oidc-client-ts";
import { createContext } from "react";

interface AuthContextType {
  user: User | null;
  signinRedirect: () => Promise<void>;
  signoutRedirect: () => Promise<void>;
}

export const AuthContext = createContext<AuthContextType>(null!);
