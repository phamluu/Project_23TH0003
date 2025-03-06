function openTab(event, TabName) {
    const Contents = document.querySelectorAll('.tab-content');
    Contents.forEach(content => {
        content.classList.remove('active');
    });
    const Buttons = document.querySelectorAll('.tab-button');
    Buttons.forEach(button => {
        button.classList.remove('active');
        button.style.backgroundColor = '#f1f1f1';
    });

    document.getElementById(TabName).classList.add('active');
    event.currentTarget.classList.add('active');
    event.currentTarget.style.backgroundColor = '#ddd';
}

function openParentTab(event, parentTabName) {
    const parentContents = document.querySelectorAll('.parent-tab-content');
    parentContents.forEach(content => {
        content.classList.remove('active');
    });
    const parentButtons = document.querySelectorAll('.parent-tab-button');
    parentButtons.forEach(button => {
        button.classList.remove('active'); 
        button.style.backgroundColor = '#f1f1f1'; 
    });

    document.getElementById(parentTabName).classList.add('active');
    event.currentTarget.classList.add('active');
    event.currentTarget.style.backgroundColor = '#ddd';
    const firstChildTab = document.querySelector(`#${parentTabName} .child-tab-button`);
    if (firstChildTab) {
        firstChildTab.click(); 
    }
}

function openChildTab(event, childTabName) {
    const childContents = document.querySelectorAll('.child-tab-content');
    childContents.forEach(content => {
        content.classList.remove('active');
    });

    const childButtons = document.querySelectorAll('.child-tab-button');
    childButtons.forEach(button => {
        button.classList.remove('active'); 
        button.style.backgroundColor = '#f1f1f1'; 
    });
    document.getElementById(childTabName).classList.add('active');
    event.currentTarget.classList.add('active');
    event.currentTarget.style.backgroundColor = '#ddd';
}

document.addEventListener('DOMContentLoaded', function () {
    const firstTab = document.querySelector('.tab-button');
    if (firstTab) {
        firstTab.click();
    }
    const firstParentTab = document.querySelector('.parent-tab-button');
    if (firstParentTab) {
        firstParentTab.click(); 
    }
});
