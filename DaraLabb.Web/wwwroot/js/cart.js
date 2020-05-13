window.onload = (event) => {
    CartCount();
}

function CartCount() {
    fetch("https://localhost:44302/api/cart/CartCount")
        .then(response => {
            console.log(response);
            if (response.ok) {
                return response.text();
            }

        })
        .then(data => {
            var element = document.getElementById("cartItem-counter");
            element.innerHTML = data
        });
}

function AddToCart(productId) {
    fetch("https://localhost:44302/api/cart/addtocart?id=" + productId)
        .then(response => {
            console.log(response);
            if (response.ok){
                return response.text();
            }
        })
        .then(data => {
            var element = document.getElementById("cartItem-counter");
            element.innerHTML = data
        });
}