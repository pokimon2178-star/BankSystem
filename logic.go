package main

import (
	"fmt"
	"math/rand"
	"time"
)

func NewAccount(owner string, balance float64, currency string) *BankAccount {
	rand.Seed(time.Now().UnixNano())
	id := fmt.Sprintf("%08d", rand.Intn(100000000))

	return &BankAccount{
		AccountID: id,
		Owner:     owner,
		Balance:   balance,
		Currency:  currency,
		Status:    StatusActive,
	}
}

func (a *BankAccount) Deposit(amount float64) {
	if a.Status == StatusFrozen {
		fmt.Println("ОШИБКА: Счет заморожен!")
		return
	}
	if amount <= 0 {
		fmt.Println("ОШИБКА: Сумма должна быть > 0")
		return
	}
	a.Balance += amount
	fmt.Printf("Пополнено на %.2f. Баланс: %.2f\n", amount, a.Balance)
}

func (a *BankAccount) Withdraw(amount float64) {
	if a.Status == StatusFrozen {
		fmt.Println("ОШИБКА: Счет заморожен!")
		return
	}
	if amount > a.Balance {
		fmt.Println("ОШИБКА: Мало денег!")
		return
	}
	a.Balance -= amount
	fmt.Printf("Снято %.2f. Остаток: %.2f\n", amount, a.Balance)
}

func (a *BankAccount) ShowInfo() {
	fmt.Printf("\n--- СЧЕТ: %s ---\nВладелец: %s\nБаланс: %.2f %s\nСтатус: %s\n----------------\n",
		a.AccountID, a.Owner, a.Balance, a.Currency, a.Status)
}
