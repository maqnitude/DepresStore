import { useEffect } from "react";
import { userManager } from "./AuthService";

export default function LogoutCallback() {
  useEffect(() => {
    userManager
      .signoutCallback()
      .then(() => {
        window.location.replace("/");
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  return <div>Signing out...</div>;
}
