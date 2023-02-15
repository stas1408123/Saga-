# Saga Choreography implementation

## Basic review

There is a Warehouse microservice. This service is responsible for storing products.

***Product*** can have:
- Id;
- Name;
- Description;
- Quantity;
- Category;
- StockStatus.

***Category*** can have:
- Id;
- Name;
- Description;
- ListOfProucts;
- LowStock;
- OutOfStock.

***Order*** can have:
- Id;
- ProductId;
- ProductAmount;
- OrderStatus;
- Date.

> StockStatus is calculated depending on the Category of the product. Example:
> 
> **In Stock**: Product Quantity > Category LowStock
> 
> **Low Stock**: CategoryLowStock >= Product Quantity > Category
> 
> **OutOfStock**: Product Quantity <= Category OutOfStock

There are 4 basic states for ***OrderStatus***:
1. Pending
1. Approved
1. InReview
1. Declined

Besides these statuses, *Failed* one was also added. The main purpose is use it to mark orders as failed if something went wrong while their processing.

## Workflow

1. User creates an Order for product
2. Order service checks and validates the order
3. Order service publishes *OrderStarted* event
4. Product service checks the *StockStatus* for product and depending on the status it will behave one of these following ways:
 
- **InStock**
  
  Reserves product and publishes *ProductInStockEvent*. OrderService changes order status to *Approved* and publishes *OrderApprovedEvent*. ProductService consumes it and finishes order.
  
- **LowStock**

  Reserves product and publishes *ProductLowStockEvent*. OrderService changes order status to *InReview* and publishes *OrderInReviewEvent*. ProductService consumes it, reserves the product and finishes order processing by now. In any time, manager can manually decline or approve this order. If the order status was changed to *Declined*, the status is changed in OrderService. Then it publishes *OrderDeclinedEvent*. ProducerService consumes it and returns products from the reservation. If manager approved the order, the status is changed into *Approved*, *OrderApprovedEvent* is published and the order processing finishes.

- **OutOfStock**

  Offer a user to wait when someone adds more products and resolve this order when product will go to *LowStock*. Or if user don’t want to wait, just decline this order. So, ProductServices publishes *OutOfStockEvent*, OrderService consumes it and doesn't updates order state (order stays in *Pending* state). Like in *LowStock*, order can be manually declined, and the flow is the same.
  
> ***Note***: The request to make order is validated in OrderService, then in ProductService we check if such product exists and if the quantity we need to reserve is valid or not. If validation failed in ProductService, it publishes *InvalidOrderDetailsEvent* and OrderService sets the status of the order as **FAILED**.

If the exception is thrown in either OrderService or ProductService, *FaultEvent* is published and the changes are reverted.

## Stack

- ASP.NET Core 7
- RabbitMQ
- MassTransit
- PostgreSQL
- MongoDB
- Swagger
