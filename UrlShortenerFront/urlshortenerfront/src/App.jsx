import './App.css'
import Table from "./Table.jsx"

function App() {
    let columns = ['full', 'short'];

  return (
    <>
          <Table name="url" columns={columns} createFields={["full", "createdById"]} />
    </>
  )
}

export default App
