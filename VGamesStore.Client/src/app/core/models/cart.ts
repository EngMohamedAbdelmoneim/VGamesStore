import { CartItem } from "./cart-item";

export interface Cart{
  id: string;
  items: CartItem[];
  // totalPrice: number;
  // totalItems: number;
  // createdAt: Date;
  // updatedAt: Date;
}
