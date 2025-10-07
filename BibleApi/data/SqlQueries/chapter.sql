-- name: GetBookChapterVerses
select v.book_id as bookId, v.chapter as chapterNumber,	v.id as verseId, v.verse as verseNumber, v.text
from verse v
where v.book_id = @bookId
and v.chapter = @chapterNumber