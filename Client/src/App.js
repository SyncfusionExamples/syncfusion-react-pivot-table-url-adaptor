import * as React from 'react';
import { PivotViewComponent } from '@syncfusion/ej2-react-pivotview';
import { DataManager, UrlAdaptor } from '@syncfusion/ej2-data';
import './App.css';

function App() {
    const data = new DataManager({
        url: 'http://localhost:5193/api/Orders', // Replace 5193 with the backend port.
        insertUrl: 'http://localhost:5193/api/Orders/Insert',
        updateUrl: 'http://localhost:5193/api/Orders/Update',
        removeUrl: 'http://localhost:5193/api/Orders/Remove',
        adaptor: new UrlAdaptor()                // Specify UrlAdaptor for custom REST API.
    });

    const editSettings = {
        allowEditing: true,    // Enables the Edit button and allows users to modify existing records
        allowAdding: true,     // Enables the Add button and allows users to create new records
        allowDeleting: true,   // Enables the Delete button and allows users to remove records
        mode: 'Normal'         // Uses Normal mode (popup dialog) for editing; other options: 'Dialog', 'Batch'
    };

    const dataSourceSettings = {
        dataSource: data,
        expandAll: false,
        rows: [{ name: 'OrderID' }],
        columns: [{ name: 'CustomerID' }],
        values: [{ name: 'Freight' }],
        formatSettings: [{ name: 'Freight', format: 'N0' }],
    };
    let pivotObj;
    // Configure beginDrillThrough event to set the primary key for CRUD operations
    function beginDrillThrough(args) {
        // Iterate through all columns in the drill-through grid
        for (var i = 0; i < args.gridObj.columns.length; i++) {
            // Check if the current column is the primary key column
            if (args.gridObj.columns[i].field === "OrderID") {
                // Mark this column as the primary key
                // This tells DataManager to use this column's value to uniquely identify records
                args.gridObj.columns[i].isPrimaryKey = true;
            } else {
                // Make all other columns visible so users can view and edit them
                args.gridObj.columns[i].visible = true;
                // Configure the edit type for date field to use a date picker for editing
                if (args.gridObj.columns[i].field === 'OrderDate' || args.gridObj.columns[i].field === 'ShippedDate') {
                    args.gridObj.columns[i].editType = 'datetimepickeredit';
                }
            }
        }
    }
    return (
        <div className='control-section' style={{ margin: 100 }}>
            <PivotViewComponent ref={d => pivotObj = d} id='PivotView' height={350} width={700} editSettings={editSettings} beginDrillThrough={beginDrillThrough} dataSourceSettings={dataSourceSettings}>
            </PivotViewComponent>
        </div>
    );
}

export default App;