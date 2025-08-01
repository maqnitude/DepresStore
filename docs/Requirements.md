# Requirements Specification

## 1. Scope

The system will include core features commonly found in e-commerce sites. Most features will be simplified to ensure the project will remain achievable while providing opportunities to practice key software engineering principles and process.

## 2. Functional Requirements

- **Customers**:

  - **Authentication**:
    - Register with an email and password
    - Log in using email and password
    - Reset password via email (forgot password?)
  - **Profile & Account Management**:
    - View and edit profile details (e.g., profile picture, display name)
    - Change email
    - Change password
    - Deactivate account
    - Subscribe/unsubscribe to promotional emails
  - **Catalog Browsing**:
    - Search products by name
    - Filter products by category, price range, etc.
    - View product details, including variants (e.g., size, color)
    - Rate and review products previously purchased
  - **Shopping Cart**:
    - Add products (including variants) to the cart
    - Remove products from the cart
    - Update item quantities in the cart
    - View total estimated cost
  - **Checkout**:
    - Enter shipping address
    - Select a shipping method (e.g., standard, express)
    - Choose a payment method
    - Confirm the order
  - **Orders Management**:
    - View order history and details
    - Cancel pending orders
    - Re-order from past orders

- **Administrators**:

  - **Authentication**:
    - Log in with admin credentials
  - **Profile Management**:
    - Change password
  - **Products Management**:
    - Create new products with variants
    - Update existing products and their variants
    - Disable products or variants
    - Delete products or variants (only if not linked to any orders)
  - **Inventory Management** (simplified):
    - Track stock level (quantities) per product variant
    - Update stock levels of product variants
  - **Categories Management**:
    - Create new categories
    - Update existing categories
    - Delete categories if contain no products
    - Disable categories along with all associated products
  - **Orders Management**:
    - View all completed and canceled orders
    - Update order status
  - **Customers**:
    - View all customers
    - View customer details and order history
    - Deactivate customer accounts
  - **Promotions** (simplified):
    - Manage contents shown in the home page of the customer-facing website
    - Apply discounts to products
    - Highlight products manually or automatically based on some criteria (e.g., sales, discounts, limited)
    - Send promotional offers via email to subscribed customers
  - **Analytics** (simplified):
    - Generate sales reports
    - Display data visualizers for metrics like page visits

## 3. Non-functional Requirements

- **Performance**:
  - Pages should load within 2 seconds under normal conditions
- **Security**:
  - Use password hashing (supported by ASP.NET Core Identity)
  - Use HTTPS only
  - JWT bearer authentication for API endpoints
- **Usability**
  - Responsive design for desktop and mobile devices
  - Simple and easy to use interface
- **Reliability**
  - Implement proper error handling and logging

# Requirements Modeling

Still working on this section

## 1. Actors

- **Customer**: Authenticated users who buy products
- **Admin**: Manages the system
- **Identity provider**: Handles user authentication
- **Payment gateway**: Processes and integrates with external payment services
- **Delivery service**: Delivers products to customers via external services

## 2. Use Cases

### 2.1 Customer Use Cases

#### Customer purchases products

#### Customer registers a new account

#### Customer logs into an existing account

#### Customer forgot password

#### Customer manages account

#### Customer edits profile

#### Customer subscribes to promotional emails

#### Customer unsubscribes from promotional emails

#### Customer changes email

#### Customer changes password

#### Customer deactivates account

#### Customer manages orders

#### Customer cancels orders

#### Customer re-orders from past orders

### 2.2 Admin Use Cases

#### Admin logs into an existing account

#### Admin manages products

#### Admin disables products

#### Admin manages categories

#### Admin disables categories

#### Admin manages orders

#### Admin updates inventory

#### Admin manages customer accounts

#### Admin manages contents on customer home page
