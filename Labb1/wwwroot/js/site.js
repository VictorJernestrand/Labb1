// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function AddToCart(productId) {
    fetch("https://localhost:44376/api/cart/addtocart?id=" + productId);
        //.then((response) => {
        //    if (response.ok) {
        //        return response.text();
        //    }
        //}).then(data => {
        //    let element = document.getElementById("cart-amount");
        //    element.innerHTML = data;
        //})
}

//function IncreaseInCart(productId) {
//    fetch("https://localhost:44376/api/cart/increaseincart?id=" + productId);
//}
//function ReduceFromCart(productId) {
//    fetch("https://localhost:44376/api/cart/reducefromcart?id=" + productId);
//}
//function RemoveFromCart(productId) {
//    fetch("https://localhost:44376/api/cart/removefromcart?id=" + productId);
//}