import {
  AppBar,
  Box,
  Divider,
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Toolbar,
  Typography,
} from "@mui/material";
import { Link, Outlet } from "react-router";

interface DrawerLinkItem {
  text: string;
  link: string;
}

const drawerWidth = 256;
const drawerLinkItems: DrawerLinkItem[] = [
  { text: "Home", link: "/" },
  { text: "Dashboard", link: "/dashboard" },
];

function MainLayout() {
  return (
    <Box sx={{ display: "flex" }}>
      <AppBar
        position="fixed"
        sx={{ width: `calc(100% - ${drawerWidth}px)`, ml: `${drawerWidth}px` }}
      >
        <Toolbar>
          <Typography variant="h6">Some Breadcrumbs</Typography>
        </Toolbar>
      </AppBar>
      <Drawer
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          "& .MuiDrawer-paper": {
            width: drawerWidth,
            boxSizing: "border-box",
          },
        }}
        variant="permanent"
        anchor="left"
      >
        <Toolbar>
          <Typography variant="h6">DepresStore.Admin</Typography>
        </Toolbar>
        <Divider />
        <List>
          {drawerLinkItems.map((item) => (
            <ListItem
              key={item.text}
              disablePadding
              component={Link}
              to={item.link}
            >
              <ListItemButton>
                <ListItemText
                  primary={
                    <Typography
                      variant="body1"
                      sx={{ color: "text.primary", fontWeight: "bold" }}
                    >
                      {item.text}
                    </Typography>
                  }
                />
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </Drawer>
      <Box component={"main"} sx={{ flexGrow: 1 }}>
        <Toolbar />
        <Outlet />
      </Box>
    </Box>
  );
}

export default MainLayout;
