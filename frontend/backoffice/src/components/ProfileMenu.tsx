import {
  Avatar,
  Box,
  Divider,
  Menu,
  MenuItem,
  Typography,
} from "@mui/material";
import { useAuth } from "../features/auth/useAuth";
import { useRef, useState } from "react";

interface ProfileMenuProps {
  onLogout: () => void;
}

export default function ProfileMenu({ onLogout }: ProfileMenuProps) {
  const [menuOpen, setMenuOpen] = useState(false);
  const anchorRef = useRef<HTMLDivElement | null>(null);

  const { user } = useAuth();
  const email = user?.profile.email;

  const handleToggleMenu = () => {
    setMenuOpen((prev) => !prev);
  };

  const handleCloseMenu = () => {
    setMenuOpen(false);
  };

  const handleLogoutClick = () => {
    onLogout();
    handleCloseMenu();
  };

  return (
    <Box sx={{ marginTop: "auto" }}>
      <Divider />
      <Box
        ref={anchorRef}
        onClick={handleToggleMenu}
        sx={{
          display: "flex",
          alignItems: "center",
          gap: 1,
          p: 2,
          cursor: "pointer",
          "&:hover": {
            backgroundColor: "action.hover",
          },
        }}
      >
        <Avatar>{email?.charAt(0).toUpperCase()}</Avatar>
        <Typography
          sx={{
            fontWeight: "bold",
            color: "text.primary",
          }}
        >
          {email}
        </Typography>
      </Box>
      <Menu
        anchorEl={anchorRef.current}
        open={menuOpen}
        onClose={handleCloseMenu}
        anchorOrigin={{ vertical: "top", horizontal: "left" }}
        transformOrigin={{ vertical: "bottom", horizontal: "left" }}
        slotProps={{
          paper: {
            sx: {
              width: anchorRef.current
                ? anchorRef.current.offsetWidth
                : undefined,
            },
          },
        }}
      >
        <MenuItem onClick={handleLogoutClick}>Sign Out</MenuItem>
      </Menu>
    </Box>
  );
}
