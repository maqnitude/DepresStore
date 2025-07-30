# Requirements

## 1. Scope

The system will include core features commonly found in e-commerce platforms. Some features will be simplified to ensure the project remains achievable while provding opportunities to practice key software development concepts and apply various techniques and patterns.

## 2. Functional Requirements

- **Customers**:

  - **Authentication**:
    - Register with an email and password
    - Log in using email and password
    - Reset password via email (forgot password?)
  - **Profile & Settings**:
    - View and edit profile details (e.g., profile picture, display name)
    - Change email
    - Change password
    - Deactivate account
    - Subscribe/unsubscribe to promotional emails
  - **Browse Catalog**:
    - Search products by name
    - Filter products by category, price range, etc.
    - View product details, including variants (e.g., size, color)
    - Rate and review products previously purchased
  - **Cart**:
    - Add products (including variants) to the cart
    - Remove products from the cart
    - Update item quantities in the cart
    - View total estimated cost
  - **Checkout**:
    - Enter shipping address
    - Select a shipping method (e.g., standard, express)
    - Choose a payment method
    - Confirm the order
  - **Orders**:
    - View order history and details
    - Cancel pending orders
    - Re-order from past orders

- **Administrators**:

  - **Authentication**:
    - Log in with admin credentials
  - **Profile**:
    - Change password
  - **Products**:
    - Create new products with variants
    - Update existing products and their variants
    - Disable products or variants
    - Delete products or variants (only if not linked to any orders)
  - **Categories**:
    - Create new categories
    - Update existing categories
    - Delete categories (if no products are assigned)
    - Disable categories (disabling all associated products)
  - **Orders**:
    - View all completed and canceled orders
    - Update order status
  - **Customers**:
    - View all customers
    - View customer details and order history
    - Deactivate customer accounts

## 3. Non-functional Requirements

- **Performance**:
  - Pages should load within 2 seconds under normal conditions
- **Security**:
  - Use ASP.NET Core Identity
  - Use HTTPS only
- **Usability**
  - Responsive design for desktop and mobile devices
  - Simple and easy to use interface
- Reliability
  - Implement proper error handling and logging
