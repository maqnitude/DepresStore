import { UserManager, WebStorageStateStore, type UserManagerSettings } from "oidc-client-ts";

const settings: UserManagerSettings = {
  authority: "https://localhost:7001",
  client_id: "backoffice",
  redirect_uri: "https://localhost:3000/login-callback",
  post_logout_redirect_uri: "https://localhost:3000/logout-callback",
  response_type: "code",
  scope: "openid email profile roles",
  userStore: new WebStorageStateStore({ store: window.localStorage })
};

export const userManager = new UserManager(settings);