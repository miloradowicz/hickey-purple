FROM alpine:latest
ARG expression

RUN apk update && apk add curl

RUN mkdir /etc/cron
WORKDIR /etc/cron
RUN echo "${expression} curl -X PUT web/reboot" > crontab

RUN crontab crontab

CMD ["crond", "-f"]
