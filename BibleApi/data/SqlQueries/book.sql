-- name: GetBooks
SELECT b.id, b.book_reference_id, b.testament_reference_id, b.name,
        max(v.chapter) as numberOfChapters
FROM book b
    inner join verse v on b.id = v.book_id
/**where**/
group by b.id, b.book_reference_id, b.testament_reference_id, b.name
