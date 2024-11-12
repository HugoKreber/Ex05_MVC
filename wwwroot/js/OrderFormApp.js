const orderFormApp = {
    data() {
        return {
            order: {
                CustomerName: '',
                Email: '',
                ShippingAddress: '',
                warehouseId: '',
                OrderStatus: 'Processing',
                totalAmount: 0,
                OrderDetails: [{
                    ArticleId: '',
                    Article: { Id: '', Name: '', Description: '', Price: '', StockQuantity:'' },
                    UnitPrice: 0,
                    Quantity: 1
                }]
            },
            articles: articlesFromServer,
            warehouses: warehousesFromServer,
            orderStatusOptions: [
                { value: 'Processing', label: 'Processing' },
                { value: 'Closed', label: 'Closed' },
                { value: 'ToOpen', label: 'To Open' }
            ]
        };
    },

    methods: {
        addOrderDetail() {
            this.order.OrderDetails.push({
                ArticleId: '',
                Article: { Name: '', Description: '', Price: '' },
                UnitPrice: 0,
                Quantity: 1
            });
        },
        removeOrderDetail(index) {
            this.order.OrderDetails.splice(index, 1);
        },
        updateArticleDetails(detail) {
            const selectedArticle = this.articles.find(article => article.Id === detail.ArticleId);
            if (selectedArticle) {
                detail.Article = selectedArticle
                detail.UnitPrice = selectedArticle.Price;
            }
        },
        updateTotalAmount() {
            this.order.totalAmount = this.order.OrderDetails.reduce((total, detail) => {
                return total + (detail.UnitPrice * detail.Quantity);
            }, 0);
        },
        async submitForm() {
            try {
                const response = await fetch('/Order/Create', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(this.order)
                });

                if (response.ok) {
                    const result = await response.json();
                    if (result.redirectUrl) {
                        window.location.href = result.redirectUrl;  // Redirection côté client
                    } else {
                        alert(result.message);
                    }
                } else {
                    const error = await response.json();
                    console.error("Erreur lors de la création de la commande:", error);
                    alert("Erreur lors de la création de la commande. " + error.message);
                }
            } catch (error) {
                console.error("Erreur réseau:", error);
                alert("Erreur réseau lors de l'envoi de la commande.");
            }
        }
    },
    watch: {
        'order.OrderDetails': {
            handler: 'updateTotalAmount',
            deep: true // Observe les changements profonds (UnitPrice, Quantity, etc.)
        }
    }
};

// Montez l'application Vue
Vue.createApp(orderFormApp).mount('#orderFormApp');