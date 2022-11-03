function confirmDelete(e) {
    const confirmation = confirm("Are you sure you want to delete this?");
    if (!confirmation) e.preventDefault();
}

$('#data-in-table').DataTable({
    'paging': true,
    'lengthChange': true,
    'searching': true,
    'ordering': true,
    'info': true,
    'autoWidth': true,
    "lengthMenu": [20, 50, 100],
    "pageLength": 20,
    "order": [[0, "desc"]]
});
$('#data-in-table2').DataTable({
    'paging': true,
    'lengthChange': true,
    'searching': true,
    'ordering': true,
    'info': true,
    'autoWidth': true,
    "lengthMenu": [20, 50, 100],
    "pageLength": 20,
    "order": [[0, "desc"]]
});