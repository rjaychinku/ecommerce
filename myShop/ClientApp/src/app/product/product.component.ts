import { Component, OnInit } from '@angular/core';
import { IProduct } from '../Interfaces/IProduct';
import { ProductService } from '../shared/product.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ICartProduct } from '../Interfaces/ICartProduct';
import { IProductSize } from '../Interfaces/IProductSize';
import { IProductColour } from '../Interfaces/IProductColour';
import { trigger, transition, state, style, animate, query, stagger } from '@angular/animations';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
  animations: [    
      trigger('fadeInOut', [
      state('void', style({
        opacity: 0
      })),
      transition('void <=> *', animate(1500)),
    ])
]
})
export class ProductComponent implements OnInit {

  products: IProduct[] = [];
  sizes: IProductSize[] = [];
  colours: IProductColour[] = [];
  sharedCartProduct: ICartProduct[] = [];
  cartproducts: IProduct[] = [];
  totalCost : number;
  selectedSizeValue = {};
  selectedColourValue = {};

  constructor(public productService: ProductService, private router: Router) { }

  ngOnInit() {
    this.getAllProducts();
    this.getProductColours();
    this.getProductSizes();
    this.createAddress();
  }

  getAllProducts() {
    console.log("calling get products...");
    this.productService.getAll().subscribe((result) => {
      console.log("result was: " + result);
      this.products = result;

      //this.sharedCartProduct = { Name: "first", Price: 34, ProductId: 7, Description: "dd", Quantity: 3, Size: "M" };
      //console.log(this.sharedCartProduct.Name);
      //this.productService.setMessage(this.sharedCartProduct);

      this.productService.getMessage.subscribe(messageProduct => {
        this.sharedCartProduct = messageProduct;
      });

    }, error => console.error(error));
  }

  getProductSizes() {
    console.log("calling get product sizes...");
    this.productService.getAllSizes().subscribe((result) => {
      console.log("result was: " + result);
      this.sizes = result;
      this.selectedSizeValue = this.sizes.find(c => c.name.toLowerCase() === "size");

    }, error => console.error(error));
  }

  getProductColours() {
    console.log("calling get product colours...");
    this.productService.getAllColours().subscribe((result) => {
      console.log("result was: " + result);
      this.colours = result;
      this.selectedColourValue = this.colours.find(c => c.name.toLowerCase() === "colour");

    }, error => console.error(error));
  }

  createAddress() {
    console.log("calling create Adrress...");
    this.productService.getCountries().subscribe((result) => {
      console.log("countries result was: " + result);
      var r = result;

    }, error => console.error(error));
  }

  addToCart(cartproduct: IProduct) {
    const newCartProduct = Object.assign({}, cartproduct)
    //const tr = JSON.parse(JSON.stringify(cartproduct)); //deep copy
    var arrSize = this.cartproducts.push(newCartProduct);
    this.totalCost += newCartProduct.price;
    console.log('ADD: The cart has ' + arrSize + ' items in it.');
  }

  removeFromCart(cartproduct: IProduct) {
    this.cartproducts =  this.cartproducts.filter(c => c !== cartproduct);
    this.totalCost -= cartproduct.price;
    console.log('DELETE: The cart has ' + this.cartproducts.length + ' items in it now.');
  }

  onSelectedSizeChange(theeproduct: IProduct, size: any) {
    // do something else with the value
    console.log(size.name);
    console.log(theeproduct.name);
    // remember to update the selectedValue
    theeproduct.size = size.name;
  }

  onSelectedColourChange(theeproduct: IProduct, colour: any) {
    // do something else with the value
    console.log(colour.name);
    console.log(theeproduct.name);
    // remember to update the selectedValue
    theeproduct.colour  = colour.name;
  }
}
