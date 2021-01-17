export default {
  oidc: {
    authority: "https://localhost:5001",
    client_id: "js",
    redirect_uri: "http://localhost:5500/callback.html",
    response_type: "code",
    scope: "openid profile",
    post_logout_redirect_uri: "http://localhost:5500/index.html",
  },
};
