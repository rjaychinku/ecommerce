import { Component, OnInit } from "@angular/core";
import { IProduct } from "../Interfaces/IProduct";
import { ProductService } from "../shared/product.service";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { ICartProduct } from "../Interfaces/ICartProduct";
import { IProductSize } from "../Interfaces/IProductSize";
import { IProductColour } from "../Interfaces/IProductColour";
import {
  trigger,
  transition,
  state,
  style,
  animate,
  query,
  stagger,
} from "@angular/animations";

@Component({
  selector: "app-product",
  templateUrl: "./product.component.html",
  styleUrls: ["./product.component.css"],
  animations: [
    trigger("fadeInOut", [
      state(
        "void",
        style({
          opacity: 0,
        })
      ),
      transition("void <=> *", animate(1000)),
    ]),
  ],
})
export class ProductComponent implements OnInit {
  products: IProduct[] = [];
  sizes: IProductSize[] = [];
  colours: IProductColour[] = [];
  sharedCartProduct: IProduct[] = [];
  cartproducts: IProduct[] = [];
  totalCost: number = 0;
  selectedSizeValue = {};
  selectedColourValue = {};

  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit() {
    this.getAllProducts();
    this.getProductColours();
    this.getProductSizes();
    this.createAddress();
  }

  async getAllProducts() {
    try {
      console.log("calling get products...");
      this.products = await this.productService.getAll();
      console.log("result was: " + this.products);

      //share
      this.productService.getMessage.subscribe((messageProduct) => {
        this.cartproducts = messageProduct;
      });
    } catch (err) {
      console.error(err);
    }

  }

  async getProductSizes() {
    try {
      console.log("calling get product sizes...");
      const result = await this.productService.getAllSizes();
      console.log("result was: " + result);
      this.sizes = result;
      this.selectedSizeValue = this.sizes.find(c => c.name.toLowerCase() === "size");
    } catch (err) {
      console.error(err);
    }
  }

  async getProductColours() {
    try {
      console.log("calling get product colours...");
      let result = await this.productService.getAllColours();
      this.colours = result;
      this.selectedColourValue = this.colours.find((c) => c.name.toLowerCase() === "colour");
    } catch (err) {
      console.error(err);
    }
  }

  createAddress() {
    console.log("calling create Adrress...");
    this.productService.getCountries().subscribe(
      (result) => {
        console.log("countries result was: " + result);
        var r = result;
      },
      (error) => console.error(error)
    );
  }

  addToCart(cartproduct: IProduct) {
    const newCartProduct = Object.assign({}, cartproduct);
    //const tr = JSON.parse(JSON.stringify(cartproduct)); //deep copy
    var arrSize = this.cartproducts.push(newCartProduct);
    this.totalCost += Number(newCartProduct.price);

    this.productService.setMessage(this.cartproducts);
    console.log("ADD: The cart has " + arrSize + " items in it.");
  }

  removeFromCart(cartproduct: IProduct) {
    this.cartproducts = this.cartproducts.filter((c) => c !== cartproduct);
    this.totalCost -= Number(cartproduct.price);
    this.productService.setMessage(this.cartproducts);
    console.log(
      "DELETE: The cart has " + this.cartproducts.length + " items in it now."
    );
  }

  onSelectedSizeChange(theeproduct: IProduct, size: any) {
    // remember to update the selectedValue
    theeproduct.size = size.name;
  }

  onSelectedColourChange(theeproduct: IProduct, colour: any) {
    // remember to update the selectedValue
    theeproduct.colour = colour.name;
  }
}
