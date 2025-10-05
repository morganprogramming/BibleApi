-- name: GetBooks
SELECT id, book_reference_id, testament_reference_id, name FROM book;

-- name: GetBookById
SELECT id, book_reference_id, testament_reference_id, name FROM book WHERE id = @bookId;