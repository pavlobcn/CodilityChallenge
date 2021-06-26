SELECT
    COALESCE(SUM(
                         (
                             SELECT
                                 MIN(m)
                             FROM
                                 (
                                     SELECT p m FROM
                                         (
                                             select l p from segments
                                             UNION
                                             select r p from segments
                                         )
                                     WHERE
                                             m > main.p
                                 )
                         ) - p), 0)
FROM
    (
        SELECT
            DISTINCT p
        FROM
            (
                select l p from segments
                UNION
                select r p from segments
            )
    ) main
WHERE
    EXISTS(SELECT * FROM segments WHERE main.p >= l AND main.p + 1 <= r)