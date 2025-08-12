import type { JSX } from "@emotion/react/jsx-runtime";
import { useAuth } from "../features/auth/useAuth";
import { Navigate } from "react-router";

export default function PublicOnlyRoute({
  children,
}: {
  children: JSX.Element;
}) {
  const { user } = useAuth();

  if (user) {
    return <Navigate to="/" replace />;
  }

  return children;
}
