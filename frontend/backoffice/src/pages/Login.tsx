import { Button, Container, Typography } from "@mui/material";
import { useAuth } from "../features/auth/useAuth";

export default function Login() {
  const { signinRedirect } = useAuth();
  return (
    <Container
      sx={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        minHeight: "100vh",
      }}
    >
      <Typography variant="h6" gutterBottom>
        Please sign in using admin credentials.
      </Typography>
      <Button variant="contained" onClick={signinRedirect}>
        Sign In
      </Button>
    </Container>
  );
}
