import { createId } from "@paralleldrive/cuid2";
export interface Cart {
    id: string
    items: CartItem[]
}

export interface CartItem {
    id: number
    pictureUrl: string
    productName: string
    price: number
    type: string
    quantity: number
    brand: string
}
export class Cart implements Cart {
    id = createId();
    items: CartItem[] = [];
}