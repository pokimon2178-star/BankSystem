package main

import "fmt"

func main() {
	var name, curr string
	var bal float64

	fmt.Print("Имя: ")
	fmt.Scanln(&name)
	fmt.Print("Валюта: ")
	fmt.Scanln(&curr)
	fmt.Print("Баланс: ")
	fmt.Scanln(&bal)

	acc := NewAccount(name, bal, curr)

	for {
		fmt.Println("\n1: Депозит, 2: Снять, 3: Статус, 4: Инфо, 0: Выход")
		var choice string
		fmt.Scanln(&choice)

		if choice == "0" {
			break
		}

		switch choice {
		case "1":
			var amt float64
			fmt.Print("Сумма: ")
			fmt.Scanln(&amt)
			acc.Deposit(amt)
		case "2":
			var amt float64
			fmt.Print("Сумма: ")
			fmt.Scanln(&amt)
			acc.Withdraw(amt)
		case "3":
			if acc.Status == StatusActive {
				acc.Status = StatusFrozen
			} else {
				acc.Status = StatusActive
			}
			fmt.Println("Новый статус:", acc.Status)
		case "4":
			acc.ShowInfo()
		}
	}
}
