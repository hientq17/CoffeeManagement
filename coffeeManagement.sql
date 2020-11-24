CREATE TABLE `Employee` (
  `employeeUser` varchar(16) NOT NULL,
  `password` varchar(16) NOT NULL,
  `employeeName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `roleId` int NOT NULL,
  `employeeStatus` bit NOT NULL,
  PRIMARY KEY (`employeeUser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

Create TABLE `CoffeeTable`(
	`tableId` int NOT NULL,
	`tableStatus` bit NOT NULL,
	PRIMARY KEY (`tableId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE `Invoice` (
  `invoiceId` int NOT NULL AUTO_INCREMENT,
  `tableId` int NOT NULL,
  `dateSale` date NOT NULL,
  `totalPayment` double DEFAULT NULL,
  `employeeUser` varchar(16) NOT NULL,
  `invoiceStatus` bit NOT NULL,
  PRIMARY KEY (`invoiceId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `InvoiceDetail` (
  `invoiceDetailId` int NOT NULL AUTO_INCREMENT,
  `invoiceId` int NOT NULL,
  `productId` int NOT NULL,
  `productAmount` int NOT NULL,
  `totalPrice` double NOT NULL,
  PRIMARY KEY (`invoiceDetailId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `Product` (
  `productId` int NOT NULL AUTO_INCREMENT,
  `productName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `unitPrice` double NOT NULL,
  `typeId` int NOT NULL,
  `productStatus` bit NOT NULL,
  PRIMARY KEY (`productId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

CREATE TABLE `ProductType` (
  `typeId` int NOT NULL AUTO_INCREMENT,
  `typeName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `typeStatus` bit NOT NULL,
  PRIMARY KEY (`typeId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

ALTER TABLE `Product` ADD  FOREIGN KEY (`typeId`) REFERENCES `ProductType`(`typeId`);

ALTER TABLE `Invoice` ADD FOREIGN KEY (`employeeUser`) REFERENCES `Employee` (`employeeUser`);

ALTER TABLE `Invoice` ADD FOREIGN KEY (`tableId`) REFERENCES `CoffeeTable` (`tableId`);

ALTER TABLE `InvoiceDetail` ADD FOREIGN KEY (`invoiceId`) REFERENCES `Invoice` (`invoiceId`);

ALTER TABLE `InvoiceDetail` ADD FOREIGN KEY (`productId`) REFERENCES `Product` (`productId`);

-- drop procedure insert_InvoiceDetail
delimiter //
Create procedure insert_InvoiceDetail(IN invoiceId int, IN productId int, IN productAmount int)
BEGIN	
	Declare COUNT int default 0;
    Declare TOTALPRICE double default 0;
    Declare TOTALAMOUNT int default 0;
    set Count = (Select count(productId) from InvoiceDetail where InvoiceDetail.invoiceId = invoiceId and InvoiceDetail.productId = productId);
    if COUNT>0 then 
			set TOTALAMOUNT = productAmount + (Select InvoiceDetail.productAmount from InvoiceDetail where InvoiceDetail.invoiceId = invoiceId and InvoiceDetail.productId = productId);
			set TOTALPRICE = TOTALAMOUNT * (Select Product.Unitprice from Product where Product.productId = productId);
			Select 'Update ',InvoiceId as 'InvoiceId', ProductId as 'ProductId', TOTALAMOUNT as 'TotalAmount', TOTALPRICE as 'TotalPrice';
            Update InvoiceDetail set InvoiceDetail.productAmount = TOTALAMOUNT, InvoiceDetail.totalPrice = TOTALPRICE  where InvoiceDetail.invoiceId=invoiceId and InvoiceDetail.productId=productId;
    else
			set TOTALPRICE = productAmount*(Select Product.Unitprice from Product where Product.productId = productId);
            Select 'Insert ',InvoiceId as 'InvoiceId', ProductId as 'ProductId', ProductAmount as 'ProductAmount', TOTALPRICE as 'TotalPrice';
			Insert into InvoiceDetail(InvoiceId,ProductId,ProductAmount,TotalPrice) values(invoiceId,productId,productAmount,TOTALPRICE);
    end if;
END//
delimiter ;	

-- drop trigger trigger_insert_InvoiceDetail
delimiter //
Create trigger trigger_insert_InvoiceDetail after insert on InvoiceDetail for each row
BEGIN
	declare TOTALPAYMENT double default 0;
	set TOTALPAYMENT =  (Select sum(InvoiceDetail.TotalPrice) from InvoiceDetail where InvoiceDetail.invoiceId = new.invoiceId group by InvoiceDetail.invoiceId);
	update Invoice set TotalPayment = TOTALPAYMENT where Invoice.invoiceId = new.invoiceId; 
END//

-- drop trigger trigger_update_InvoiceDetail
delimiter //
Create trigger trigger_update_InvoiceDetail after update on InvoiceDetail for each row
BEGIN
	declare TOTALPAYMENT double default 0;
	set TOTALPAYMENT =  (Select sum(InvoiceDetail.TotalPrice) from InvoiceDetail where InvoiceDetail.invoiceId = new.invoiceId group by InvoiceDetail.invoiceId);
	update Invoice set TotalPayment = TOTALPAYMENT where Invoice.invoiceId = new.invoiceId; 
END//

delimiter ;
Insert INTO `CoffeeTable` VALUES (1,0),(2,1);
INSERT INTO `Employee` VALUES ('da140080','12345678','Nguyen Lam Giang',1),('de140069','12345678','Huynh Long',0),('de140248','12345678','Trinh Quang Hien',0);
INSERT INTO `ProductType` VALUES (1,'Cafe'),(2,'Trà sữa'),(3,'Trà'),(4,'Bánh ngọt');
INSERT INTO `Product` VALUES (1,'Cà phê đen',12000,1),(2,'Trà gừng',15000,3),(3,'Trà sữa truyền thống',18000,2),(4,'Bánh bông lan',15000,4);
INSERT INTO `Invoice` VALUES (3,1,'2020-11-12',0,'de140069'),(4,2,'2020-11-13',0,'da140080');
INSERT INTO `InvoiceDetail` VALUES (3,3,1,3,36000),(4,3,2,1,15000),(5,4,3,1,18000);




