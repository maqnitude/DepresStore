import { Route, Routes } from "react-router";
import MainLayout from "./components/MainLayout";
import Dashboard from "./pages/Dashboard";
import Products from "./pages/Products";
import Categories from "./pages/Categories";
import Customers from "./pages/Customers";
import Orders from "./pages/Orders";

function App() {
  return (
    <Routes>
      <Route element={<MainLayout />}>
        <Route path="/">
          <Route index element={<Dashboard />}></Route>
          <Route path="dashboard" element={<Dashboard />}></Route>
          <Route path="products" element={<Products />}></Route>
          <Route path="categories" element={<Categories />}></Route>
          <Route path="customers" element={<Customers />}></Route>
          <Route path="orders" element={<Orders />}></Route>
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
