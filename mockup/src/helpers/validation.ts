
export const rules = {

    file: [(v: File[]) => v.length || "You must enter a file"],
    required: (v: any) => !!v || 'Field Required',
    maxLength: (l: number) => (v: string) => v && v.length <= l || `Max length ${l}`,
    currency: (v: string) => /^[+-]?[0-9]{1,3}(?:,?[0-9]{3})*(?:\.[0-9]{2})?$/.test(v) || "Currency format required"
}