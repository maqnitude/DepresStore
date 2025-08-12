import { useEffect } from "react";
import { userManager } from "./AuthService";

export default function LoginCallback() {
  useEffect(() => {
    userManager
      .signinRedirectCallback()
      .then(() => {
        window.location.replace("/");
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  return <div>Signing in...</div>;
}
