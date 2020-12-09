import { Component, OnInit } from '@angular/core';
import { ProductService } from '../shared/product.service';
import { ICartProduct } from '../Interfaces/ICartProduct';
import { UseraccountService } from '../shared/useraccount.service';
import { IProduct } from '../Interfaces/IProduct';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {
  sharedCartProduct: IProduct[] = [];

  constructor(private productService: ProductService, public useraccountService: UseraccountService,) { }

  ngOnInit() {
    this.useraccountService.checkoutFormModel.reset();

    this.productService.getMessage.subscribe(messageProduct  => {
      this.sharedCartProduct = messageProduct;
    });
  }

  newMessage() {
      let cartProduct: IProduct = {
      name: "Testing!!!",
      description: "A teswt desc",
      price: 76,
      size: "XS",
      productId: "3",
      quantity: 3,
      colour: "purple"
    };

    console.log(cartProduct.name);
    this.sharedCartProduct.push(cartProduct);
    this.productService.setMessage(this.sharedCartProduct);
  }

  checkout() {
    let cartProduct: IProduct = {
      name: "Testing two!",
      description: "A testing description",
      price: 180,
      size: "M",
      productId: "4",
      quantity: 5,
      colour: "orange"
  };

  console.log(cartProduct.name);
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
