import Table from "./Table.jsx"

export default function Info()
{
    let columns = ['full', 'short', 'createdById', 'createdAt'];

    return (
        <Table name="url" columns={columns} />
    )
}