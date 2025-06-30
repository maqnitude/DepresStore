import {
  Sidebar,
  SidebarContent,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
} from "@/components/ui/sidebar";
import {
  BoxIcon,
  GaugeIcon,
  ScrollTextIcon,
  TagIcon,
  UsersIcon,
} from "lucide-react";
import { NavLink } from "react-router";

const items = [
  {
    title: "Dashboard",
    url: "/dashboard",
    icon: GaugeIcon,
  },
  {
    title: "Products",
    url: "/products",
    icon: BoxIcon,
  },
  {
    title: "Categories",
    url: "/categories",
    icon: TagIcon,
  },
  {
    title: "Customers",
    url: "/customers",
    icon: UsersIcon,
  },
  {
    title: "Orders",
    url: "/orders",
    icon: ScrollTextIcon,
  },
];

function AppSidebar() {
  return (
    <Sidebar>
      <SidebarContent>
        <SidebarGroup>
          <SidebarGroupLabel className="text-xl font-bold mx-auto">
            GDevShop
          </SidebarGroupLabel>
          <SidebarGroupContent>
            <SidebarMenu>
              {items.map((item) => (
                <SidebarMenuItem key={item.title}>
                  <SidebarMenuButton asChild>
                    <NavLink to={item.url}>
                      <item.icon />
                      <span>{item.title}</span>
                    </NavLink>
                  </SidebarMenuButton>
                </SidebarMenuItem>
              ))}
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>
    </Sidebar>
  );
}

export default AppSidebar;
