import { Invoice, InvoiceDraft } from "../models/Invoice"
import axiosInstance from "./httpFetcher"

export async function fetchInvoices(term: string = "") {
  try {
    const { data } = await axiosInstance.get(`invoices/?Term=${term}`)
    return data
  } catch (e) {
    throw e
  }
}

export async function createInvoice(invoice: InvoiceDraft) {
  try {
    const { data } = await axiosInstance.post('invoices', invoice)
    return data
  } catch (e) {
    throw e
  }
}

export async function updateInvoice(invoice: Invoice) {
  try {
    const { data } = await axiosInstance.put('invoices', invoice)
    return data
  } catch (e) {
    throw e
  }
}

export async function deleteInvoice(id: Invoice['id']) {
  try {
    const { data } = await axiosInstance.delete(`invoices/${id}`)
    return data
  } catch (e) {
    throw e
  }

}

export async function uploadFile(id: Invoice['id'], file: File) {
  try {
    const formData = new FormData();
    formData.append("id", id.toString());
    formData.append("file", file);
    const { data } = await axiosInstance.post(`upload/`, formData)
    return data
  } catch (e) {
    throw e
  }

}
