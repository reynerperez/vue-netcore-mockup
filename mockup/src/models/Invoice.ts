export type Invoice = {
    id: number
    title: string,
    date: Date,
    amount: string,
    file: string
}

export type InvoiceDraft = Omit<Invoice, 'id'>