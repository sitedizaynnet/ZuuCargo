const sql = require('mssql')

async () => {
    try {
        const pool = await sql.connect('mssql://sa:123@localhost/KolayGps')
        const result = await sql.query`select * from gpsdata where id = ${value}`
        console.dir(result)
    } catch (err) {
        // ... error checks
    }
}