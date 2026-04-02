package main

const (
	StatusActive = "Active"
	StatusFrozen = "Frozen"
)

type BankAccount struct {
	AccountID string
	Owner     string
	Balance   float64
	Currency  string
	Status    string
}
