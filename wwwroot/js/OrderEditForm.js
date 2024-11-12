const orderEditForm = {
    data() {
        return {
            order: initialOrderModel, // Initialiser avec le modèle de commande existant
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
                Article: { Id: '', Name: '', Description: '', Price: '', StockQuantity: '' },
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
                detail.Article = selectedArticle;
                detail.UnitPrice = selectedArticle.Price;
            }
        },
        updateTotalAmount() {
            console.log("updateAmount");
            this.order.totalAmount = this.order.OrderDetails.reduce((total, detail) => {
                return total + (detail.UnitPrice * detail.Quantity);
            }, 0);
        },
        async submitForm() {
            try {
                const response = await fetch('/Order/Edit', { // Assurez-vous que le nom de l'action est correct
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(this.order)
                });

                if (response.ok) {
                    const result = await response.json();
                   
                    window.location.href = "/Order/Index"; // Redirige après succès
                } else {
                    const error = await response.json();
                    console.error("Erreur lors de la modification de la commande:", error);
                    alert("Erreur lors de la modification de la commande. " + error.message);
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
    },
    mounted() {
        // Mettre à jour le total dès que les données sont montées
        this.updateTotalAmount();
    }
};

// Montez l'application Vue
Vue.createApp(orderEditForm).mount('#orderEditForm');