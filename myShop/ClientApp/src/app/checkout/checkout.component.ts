import { Component, OnInit } from '@angular/core';
import { ProductService } from '../shared/product.service';
import { ICartProduct } from '../Interfaces/ICartProduct';
import { UseraccountService } from '../shared/useraccount.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {
  sharedCartProduct: ICartProduct[] = [];

  constructor(private productService: ProductService, public useraccountService: UseraccountService,) { }

  ngOnInit() {
    this.useraccountService.checkoutFormModel.reset();

    this.productService.getMessage.subscribe(messageProduct => {
      this.sharedCartProduct = messageProduct;
    });
  }

  newMessage() {
      const cartProduct: ICartProduct = {
      Name: "Testing!",
      Description: "A teswt desc",
      Price: 76,
      Size: "XS",
      ProductId: 3,
      Quantity: 3
    };

    console.log(cartProduct.Name);
    this.sharedCartProduct.push(cartProduct);
    this.productService.setMessage( this.sharedCartProduct);
  }

  checkout() {
    const cartProduct: ICartProduct = {
    Name: "Testing!",
    Description: "A teswt desc",
    Price: 76,
    Size: "XS",
    ProductId: 3,
    Quantity: 3
  };

  console.log(cartProduct.Name);
  this.sharedCartProduct.push(cartProduct);
  this.productService.setMessage( this.sharedCartProduct);
}
}

interface CheckoutUser {
  firstname: string; //TODO: auto populate from registration3rd party auth
  lastname: string; //TODO: auto populate from registration3rd party auth
  username: string; //TODO: auto populate from registration3rd party auth
  email: string; //TODO: auto populate from registration3rd party auth
  //address: Address;
}

interface Address {
  aptsuite: string;
  address: string;
  city: string;
  province: string;
  country: string;
  sameasshipping: boolean;
}

interface Payment {
  cardtype: string;
  nameoncard: string;
  cvv: string;
  dateofexpiry: string;
}
