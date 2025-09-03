import { useState } from 'react'
import { simpleFetch } from './Fetch.js'
import { fetchTypes, Enums } from './Enums';

export default function Table(props)
{
    let columns = props.columns;
    let name = props.name;
    let currentUserId = Number(sessionStorage.getItem(Enums.currentUserId));
    let createFields = props.createFields;

    const [definitions, setData] = useState([]);
    const [loaded, setLoaded] = useState(false);
    if (!loaded) { 
        load(name, setData, setLoaded);
    }

    return (
        <>
            <table>
                <thead>
                    <tr>
                        {columns.map((item, idx) =>
                            <th key={idx}>{item}</th>
                        )}
                    </tr>
                </thead>
                <tbody>
                    {definitions.map((item, idx) =>
                        <tr key={idx}>
                            {columns.map((col, colIdx) => 
                                <td key={colIdx}>{item[col]}</td>
                            )}
                            { currentUserId != 0 && currentUserId == item.createdById &&
                                <td><button onClick={() => { deleteItem(item.id, name, setLoaded); setLoaded(false); }}>Delete</button></td>
                            }
                        </tr>
                    )}
                </tbody>
            </table>
            { currentUserId != 0 &&
                 <div>
                    <input type="text" id="createText" />
                    <button onClick={() => create(name, createFields, [document.getElementById("createText").value, currentUserId], setLoaded)}>Add</button>
                </div>
            }
        </>
    )
}

function load(name, setData, setLoaded) {
    if (name) {
        simpleFetch(fetchTypes.Get, name, [], [], ((res) => {
            setLoaded(true);
            setData(res)
        }), (err) => console.log(err))
    }
}

function deleteItem(id, dest, setLoaded) {
    simpleFetch(fetchTypes.Delete, dest + "/" + id, [], [], (() => {
        setLoaded(false);
    }), (err) => console.log(err))
}

function create(name, createFields, values, setLoaded) {
    simpleFetch(fetchTypes.Post, name, createFields, values, (() => { 
        setLoaded(false)
    }), (err) => { console.error(err) })
}