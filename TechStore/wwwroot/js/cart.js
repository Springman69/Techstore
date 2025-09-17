function togglePromoCode() {
    var promoCodeContainer = document.querySelector('.promo-code-input');
    promoCodeContainer.style.display = promoCodeContainer.style.display === 'block' ? 'none' : 'block';
    var arrow = document.querySelector('.promo-code-toggle .arrow');

    promoCodeContainer.classList.toggle('open');
    arrow.classList.toggle('up');
}
function applyPromoCode() {
  
} 

